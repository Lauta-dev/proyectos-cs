#!/bin/bash
build_dir="./bin/build"
arch="linux-x64"

echo "Build dir: $build_dir"
echo "Arch: $arch"
echo "Building project"

dotnet publish -c Release -r $arch --self-contained true /p:PublishSingleFile=true -o $build_dir
