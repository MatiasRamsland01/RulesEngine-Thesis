#!/bin/sh

mitmproxy2swagger -i /src/shared/flow -o /src/shared/mitm2swaggeroutput.yaml -p https://bouvet.cvpartner.com/api --format flow \
&& \
sed -i -e 's/ignore://g' /src/shared/mitm2swaggeroutput.yaml \
&& \
mitmproxy2swagger -i /src/shared/flow -o /src/shared/mitm2swaggeroutput.yaml -p https://bouvet.cvpartner.com/api --format flow
