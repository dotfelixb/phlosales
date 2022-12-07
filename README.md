Welcome to the monorepo for PhloSales

## Contents
This repository is a monorepo containing four projects
- [**src/nginx**](https://github.com/dotfelixb/phlosales/tree/main/src/nginx) is a reverse proxy serving as an api gateway, phlosales.web depends on it
- [**src/phlosales.auth**](https://github.com/dotfelixb/phlosales/tree/main/src/PhloSales.Auth) contains the source for authentication, authorization and token generation
- [**src/phlosales.server**](https://github.com/dotfelixb/phlosales/tree/main/src/PhloSales.Server) contains the source for sales order
- [**src/phlosales.web**](https://github.com/dotfelixb/phlosales/tree/main/src/phlosales.web) contains the source for a react spa frontend for sales order

### misc:
- [**src/phlosales.core**](https://github.com/dotfelixb/phlosales/tree/main/src/PhloSales.Core) contains the source for common operation between all services except data and web
- [**src/phlosales.data**](https://github.com/dotfelixb/phlosales/tree/main/src/PhloSales.Data) contains the source for data interaction

## Deployment
Download and install [**docker desktop**](https://www.docker.com/products/docker-desktop/) for windows or macos (linux need extra attention), recent version of `docker desktop` installation comes with `docker-compose` as `docker compose` added to your path

Clone and run project with the command below
```
git clone https://github.com/dotfelixb/phlosales.git
cd phlosales

docker compose -f desktop-compose.yml up 
```
`docker compose` might need to download extra images and packages, after `docker compose` is done point your browser to [phlosales web](http://localhost:3000)
```
http://localhost:3000
```
This will bring up the react app which redirect users to `/login` to authenticate

The project contains one `login` user for testing
```
username: admin@phlosales.com
password: 1433P@$$
```