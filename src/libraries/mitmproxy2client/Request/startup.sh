#!/bin/sh

cp ./certs/mitmproxy-ca-cert.pem ./usr/local/share/ca-certificates/mitmproxy-ca-cert.pem

cd ./usr/local/share/ca-certificates/mitmproxy-ca-cert.pem

update-ca-certificates

python request.py