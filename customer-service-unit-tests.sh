#!/usr/bin/env bash

set -e

KEEP_RUNNING=
USE_EXISTING_CONTAINERS=

while [ ! -z "$*" ] ; do
  case $1 in
    "--keep-running" )
      KEEP_RUNNING=yes
      ;;
    "--use-existing-containers" )
      USE_EXISTING_CONTAINERS=yes
      ;;
    "--help" )
      echo ./customer-service-unit-tests.sh --keep-running --use-existing-containers
      exit 0
      ;;
    *)
      echo ./customer-service-unit-tests.sh --keep-running --use-existing-containers
      exit 0
      ;;
  esac
  shift
done

if dotnet nuget list source | grep -q 'https://api.bintray.com/nuget/eventuateio-oss/eventuateio-dotnet-snapshots'; then
  echo "Package already exists"
else
  echo "Add package"
  dotnet nuget add source https://api.bintray.com/nuget/eventuateio-oss/eventuateio-dotnet-snapshots
fi

if [ -z "$USE_EXISTING_CONTAINERS" ] ; then
  docker-compose down
fi

docker-compose up -d db

docker-compose run --rm wait-for-db

if [ -z "$USE_EXISTING_CONTAINERS" ] ; then
  docker-compose run --rm dbsetup
fi

docker-compose up -d zookeeper
docker-compose up -d kafka
docker-compose up -d cdc-service

# Wait for docker containers to start up
./wait-for-services.sh

dotnet build CustomerService.UnitTests/CustomerService.UnitTests.csproj -c release
# Run Service
docker-compose run --rm customer-service-unittests

# Tear down test environment

if [ -z "$KEEP_RUNNING" ] ; then
  docker-compose down -v --remove-orphans
fi