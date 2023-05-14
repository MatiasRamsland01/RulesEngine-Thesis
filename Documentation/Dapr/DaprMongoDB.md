# Provisioning the MongoDB Locally

This will spin up a Local MongoDB with the specified credentials:

```bash
docker run --name adventurous -p 27017:27017 \ 
-e MONGO_INITDB_ROOT_USERNAME=admin \ 
-e MONGO_INITDB_ROOT_PASSWORD=password -d mongo
```
