const jq = require('node-jq');
const fs = require('fs');
const yaml = require('js-yaml');
const { Console } = require('console');

(async () =>
    run(
        process.env.SHARED_FOLDER_PATH + 'mitm2swaggeroutput.yaml',
        process.env.SHARED_FOLDER_PATH + 'compgenoutput.json'
    ))();

/**
 * @param inputFile Json or yaml input file
 * @param outputFile Json or yaml output file
 * @type {(inputFile: string, outputFile: string) => void}
 * @return void
 */
async function run(inputFile, outputFile) {
    let filter = '[..|.properties? | select(. != null)]';

    // Checks if input file exists, else returns 'No input file found!'
    if (!fs.existsSync(inputFile)) {
        console.log('No input file found!');
        return;
    }

    // Read file
    let file = fs.readFileSync(inputFile, 'utf-8');

    // Parse to json if file is yaml
    let json = inputFile.endsWith('yaml') ? yaml.load(file) : JSON.parse(file);

    // Creates components section if components section do not exists
    if (!json['components']) {
        json['components'] = { schemas: {} };
    }

    // Creates schemas section in components section if components section do not exists
    if (json['components'] && !json.components['schemas']) {
        json.components['schemas'] = {};
    }

    // Temporary object for holding components
    let tempComps = {};

    jq.run(filter, json, { input: 'json' }).then((output) => {
        let js = JSON.parse(output);

        // Loops through the filtered properties
        for (let val of Object.values(js)) {
            // Object values
            let jsVal = val;
            for (let key of Object.keys(jsVal)) {
                let type = jsVal[key]?.['type'];
                if (key && type) {
                    if (tempComps[key]) {
                        if (tempComps[key] === 'object' && type !== 'object') {
                            tempComps[key] = type;
                        }
                    } else {
                        tempComps[key] = type;
                    }
                }
            }
        }

        jq.run(filter + ' | add', json, { input: 'json' }).then((output) => {
            let js = JSON.parse(output);

            for (let path of Object.keys(json.paths)) {
                const regex = /\b[0-9A-Fa-f]+\b/gm;
                let lastKey = path;
                let id = 0;
                let m;

                while ((m = regex.exec(path)) !== null) {
                    if (m.index === regex.lastIndex) {
                        regex.lastIndex++;
                    }

                    m.forEach((match) => {
                        // console.log(`Found id ${id + 1}`);
                        let name = `id_${id}`;
                        lastKey = lastKey.replaceAll(`${match}`, `{${name}}`);

                        for (let type of Object.keys(json.paths[path])) {
                            if (json.paths[path][type].summary) {
                                json.paths[path][type].summary = json.paths[
                                    path
                                ][type].summary.replaceAll(
                                    `${match}`,
                                    `{${name}}`
                                );
                            }

                            if (!json.paths[path][type].parameters) {
                                json.paths[path][type].parameters = [];
                            }

                            json.paths[path][type].parameters.push({
                                name: `${name}`,
                                in: 'path',
                                required: true,
                            });
                        }
                        id++;
                    });
                }

                if (lastKey !== path) {
                    json.paths[lastKey] = json.paths[path];
                    delete json.paths[path];
                }
            }

            let components = [];

            for (let key of Object.keys(js)) {
                components[key] = js[key];
            }

            let func = (a) => a['properties'] || a['items'];

            components = Object.entries(components).sort(([, a], [, b]) => {
                return func(a) && !func(b) ? -1 : !func(a) && func(b) ? 1 : 0;
            });

            for (let [, [key, val]] of Object.entries(components)) {
                //console.log(`Found component ${key}`);

                if (tempComps[key]) {
                    val.type = tempComps[key];
                }

                let outputText = JSON.stringify(json);
                let formatKey = `"${key}":{"$ref":"#/components/schemas/${key}"}`;
                let formatObject = '{' + formatKey + '}';

                outputText = outputText.replaceAll(
                    JSON.stringify({ [key]: val }),
                    formatObject
                );
                outputText = outputText.replaceAll(
                    `"${key}":${JSON.stringify(val)}`,
                    formatKey
                );

                outputText = outputText.replaceAll(
                    JSON.stringify({ [key]: val }).replaceAll(
                        `"type":"${val.type}"`,
                        `"type":"object"`
                    ),
                    formatObject
                );
                outputText = outputText.replaceAll(
                    `"${key}":${JSON.stringify(val).replaceAll(
                        `"type":"${val.type}"`,
                        `"type":"object"`
                    )}`,
                    formatKey
                );

                json = JSON.parse(outputText);
                val.nullable = true;
                json.components.schemas[key] = val;
            }

            fs.writeFile(
                outputFile,
                outputFile.endsWith('yaml')
                    ? yaml.dump(json)
                    : JSON.stringify(json, null, 2),
                function (err) {
                    if (err) {
                        console.log(err);
                    }
                }
            );
        });
    });
}
