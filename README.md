# registration-api

## About this repository

This repository holds code to register new users, either from our own application or using a third-party platform.

## How to build & run this project

```bash
docker build -t registration-api:latest .
docker run -d -p 80:80 --name registration registration-api:latest
```
