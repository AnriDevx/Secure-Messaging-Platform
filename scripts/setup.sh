#!/bin/bash
dotnet restore
cd src/Sync.Web
npm install
cd ../..