#!/bin/bash

if [ -f "$SHARED_FOLDER_PATH""NSwagClient.cs" ]; then
    if [ "$SAVE_OLD_CS_CLIENT"=true ]; then
        DATE=$(date +'%b%d%Y%X')
        echo SAVE_OLD_CS_CLIENT=${SAVE_OLD_CS_CLIENT} - Saving old NSwagClient.cs to ${BACKUP_FOLDER_PATH}..
        mv ${SHARED_FOLDER_PATH}NSwagClient.cs ${BACKUP_FOLDER_PATH}NSwagClient${DATE}.json
    else
        echo SAVE_OLD_CS_CLIENT=${SAVE_OLD_CS_CLIENT} - Old NSwagClient.cs file is deleted..
        rm ${SHARED_FOLDER_PATH}NSwagClient.cs
    fi
fi

if [ -f "$SHARED_FOLDER_PATH""NSwagClientContract.cs" ]; then
    if [ "$SAVE_OLD_CS_CLIENT_CONTRACT"=true ]; then
        DATE=$(date +'%b%d%Y%X')
        echo SAVE_OLD_CS_CLIENT_CONTRACT=${SAVE_OLD_CS_CLIENT_CONTRACT} - Saving old NSwagClientContract.cs to ${BACKUP_FOLDER_PATH}..
        mv ${SHARED_FOLDER_PATH}NSwagClientContract.cs ${BACKUP_FOLDER_PATH}NSwagClientContract${DATE}.json
    else
        echo SAVE_OLD_CS_CLIENT_CONTRACT=${SAVE_OLD_CS_CLIENT_CONTRACT} - Old NSwagClientContract.cs file is deleted..
        rm ${SHARED_FOLDER_PATH}NSwagClientContract.cs
    fi
fi

echo Starting creating of client..
dotnet NSwag/Net70/dotnet-nswag.dll run /variables:InputSpec=/src/shared/compgenoutput.json nswagconfig.nswag
