#! /usr/bin/env bash
set -euo pipefail

image_name="mastermind:1"

docker build . -t "${image_name}" --target "test"

docker run "${image_name}"