# wificontrol
Program that turns on and off the notebook's wifi via terminal on macos
# generate app
dotnet publish -c Release -r osx-x64 --self-contained true \
    -p:PublishSingleFile=true \
    -p:EnableCompressionInSingleFile=true \
    -p:AssemblyName=wifiterm \
    -o publish



# execute binary
Script copy.sh copies a file to /usr/local/bin and applies permissions.