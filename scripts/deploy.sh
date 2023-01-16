#!/usr/bin/env bash
set -euo pipefail

image_tag=${BUILDKITE_COMMIT}

kubectl apply -f ../kube/config.yaml -n fma
kubectl set image -n fma deployment/suyash-mastermind-api-deployment suyash-mastermind=docker.myob.com/future-makers-academy/suyash-mastermind:$image_tag