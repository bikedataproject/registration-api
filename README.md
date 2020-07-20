# registration-api

## About this repository

![.NET Core](https://github.com/bikedataproject/api/workflows/.NET%20Core/badge.svg)
![Docker Image CI](https://github.com/bikedataproject/registration-api/workflows/Docker%20Image%20CI%20Build/badge.svg)
![Docker Image CD](https://github.com/bikedataproject/registration-api/workflows/Docker%20Image%20Staging%20CD/badge.svg)

This repository holds code to register new users, either from our own application or using a third-party platform.

## How to build & run this project

```bash
docker build -t registration-api:latest .
docker run -d -p 80:80 --name registration registration-api:latest
```
