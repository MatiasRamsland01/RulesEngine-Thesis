#!/bin/bash

if [ -f "$SHARED_FOLDER_PATH""flow" ]; then
    if [ "$SAVE_OLD_FLOW"=true ]; then
        DATE=$(date +'%b%d%Y%X')
        echo SAVE_OLD_FLOW=${SAVE_OLD_FLOW} - Saving old flow to ${BACKUP_FOLDER_PATH}
        mv ${SHARED_FOLDER_PATH}flow ${BACKUP_FOLDER_PATH}flow${DATE}
    else
        echo SAVE_OLD_FLOW=${SAVE_OLD_FLOW} - Old flow file is deleted..
        rm ${SHARED_FOLDER_PATH}flow
    fi
fi

nohup python kill.py &

echo Starting MitmProxy..
mitmdump -w ${SHARED_FOLDER_PATH}flow
