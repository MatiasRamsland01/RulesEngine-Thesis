FROM python:3.10-slim-buster

WORKDIR /src

RUN pip install --no-cache-dir mitmproxy2swagger

COPY . .

CMD ["/bin/bash", "./startup_mitm2swagger.sh"]