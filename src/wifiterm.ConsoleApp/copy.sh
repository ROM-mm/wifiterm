#! /bin/bash

set -e

local=$(pwd)

sudo cp ${local}/publish/wifiterm /usr/local/bin/wifiterm

# Add execute permission
sudo chmod +x /usr/local/bin/wifiterm

echo "wifiterm has been installed successfully!"


