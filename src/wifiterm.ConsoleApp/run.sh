#! /bin/bash

set -e

local=$(pwd)

# Verifica se um argumento foi passado
if [ -z "$1" ]; then
    echo "Usage: $0 [debug|release]"
    exit 1
fi

# Define comportamento baseado no parâmetro
if [ "$1" == "debug" ]; then
    rm -rf ${local}/bin
    rm -rf ${local}/obj
    dotnet run
    echo "wifiterm generated successfully in src/bin/Debug!"
elif [ "$1" == "release" ]; then
    rm -rf ${local}/publish
    rm -rf ${local}/obj
    dotnet publish -c Release -r osx-arm64 --self-contained true \
    -p:PublishSingleFile=true \
    -p:EnableCompressionInSingleFile=true \
    -p:AssemblyName=wifiterm \
    -p:DebugType=None \
    -p:DebugSymbols=false \
    -o publish
    echo "wifiterm generated successfully in src/publish!"
else
    echo "Invalid option: $1"
    echo "Usage: $0 [debug|release]"
    exit 1
fi

echo "Cleaning project..."
dotnet clean
dotnet nuget locals all --clear
rm -rf bin obj
echo "✅ Cleanup complete!"
