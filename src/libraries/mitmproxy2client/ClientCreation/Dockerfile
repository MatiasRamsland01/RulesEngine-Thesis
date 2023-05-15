FROM mcr.microsoft.com/dotnet/sdk:latest

RUN apt-get update && apt-get install -y unzip curl busybox jq && rm -rf /var/lib/apt/lists/*  \
    && curl -O -L https://github.com/RicoSuter/NSwag/releases/download/v13.18.2/NSwag.zip \
    && unzip -q ./NSwag.zip -d NSwag

COPY . .

# ENTRYPOINT ["tail", "-f", "/dev/null"]

CMD ["/bin/bash", "./startup_createclient.sh"]
