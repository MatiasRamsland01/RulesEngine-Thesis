#!/bin/sh

echo $REGEN_SPEC

if [ $REGEN_SPEC = true ]
then
    backup_dir_date=$(date +'%b%d%Y%X')
    mkdir /src/shared/${backup_dir_date}
    mv /src/shared/flow /src/shared/${backup_dir_date}/
    mv /src/shared/compgenoutput.json /src/shared/${backup_dir_date}/
    mv /src/shared/mitm2swaggeroutput.yaml /src/shared/${backup_dir_date}/
    mitmdump -w /src/shared/flow
else
    mitmdump -w /src/shared/flow
