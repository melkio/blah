FROM microsoft/dotnet:latest
MAINTAINER Alessandro Melchiori

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

COPY . /usr/src/app
 
RUN dotnet restore
RUN dotnet build

ENV ASPNETCORE_URLS http://*:5000
 
EXPOSE 5000
 
CMD ["dotnet", "run"]