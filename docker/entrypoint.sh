#!/bin/sh
set -e

dotnet WebApp.dll --migrate
exec dotnet WebApp.dll
