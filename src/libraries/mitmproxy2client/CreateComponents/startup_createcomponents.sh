#!/bin/bash

if [ -f "$SHARED_FOLDER_PATH""compgenoutput.json" ]; then
    if [ "$SAVE_OLD_COMPONENTS_GEN"=true ]; then
        DATE=$(date +'%b%d%Y%X')
        echo SAVE_OLD_COMPONENTS_GEN=${SAVE_OLD_COMPONENTS_GEN} - Saving old compgenoutput.json to ${BACKUP_FOLDER_PATH}..
        mv ${SHARED_FOLDER_PATH}compgenoutput.json ${BACKUP_FOLDER_PATH}compgenoutput${DATE}.json
    else
        echo SAVE_MITM2SWAGGER_GEN=${SAVE_OLD_COMPONENTS_GEN} - Old mitm2swaggeroutput.yaml file is deleted..
        rm ${SHARED_FOLDER_PATH}compgenoutput.json
    fi
fi

echo Starting CreateComponents..
npm start