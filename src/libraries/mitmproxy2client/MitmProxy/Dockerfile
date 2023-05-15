FROM mitmproxy/mitmproxy:latest

RUN apt-get update && apt-get install -y procps && rm -rf /var/lib/apt/lists/*

COPY . .

CMD ["/bin/bash", "./startup_mitmproxy.sh"]