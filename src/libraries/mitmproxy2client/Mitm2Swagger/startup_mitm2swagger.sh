#!/bin/bash

if [ -f "$SHARED_FOLDER_PATH""mitm2swaggeroutput.yaml" ]; then
    if [ "$SAVE_OLD_MITM2SWAGGER_GEN"=true ]; then
        DATE=$(date +'%b%d%Y%X')
        echo SAVE_OLD_MITM2SWAGGER_GEN=${SAVE_OLD_MITM2SWAGGER_GEN} - Saving old mitm2swaggeroutput to ${BACKUP_FOLDER_PATH}..
        mv ${SHARED_FOLDER_PATH}mitm2swaggeroutput.yaml ${BACKUP_FOLDER_PATH}mitm2swaggeroutput${DATE}.yaml
    else
        echo SAVE_OLD_MITM2SWAGGER_GEN=${SAVE_OLD_MITM2SWAGGER_GEN} - Old mitm2swaggeroutput.yaml file is deleted..
        rm ${SHARED_FOLDER_PATH}mitm2swaggeroutput.yaml
    fi
fi

echo Starting Mitmproxy2Swagger..
mitmproxy2swagger -i ${SHARED_FOLDER_PATH}flow -o ${SHARED_FOLDER_PATH}mitm2swaggeroutput.yaml -p ${BASE_URL} --format flow && \
sed -i -e 's/ignore://g' ${SHARED_FOLDER_PATH}mitm2swaggeroutput.yaml && \
mitmproxy2swagger -i ${SHARED_FOLDER_PATH}flow -o ${SHARED_FOLDER_PATH}mitm2swaggeroutput.yaml -p ${BASE_URL} --format flow
