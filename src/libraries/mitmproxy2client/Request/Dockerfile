FROM python:3.10-slim-buster

RUN pip install --no-cache-dir requests docker

COPY . .

CMD ["/bin/bash", "./startup_requests.sh"]