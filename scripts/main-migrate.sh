#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(realpath "$(dirname "${BASH_SOURCE[0]}")")"

pushd "${SCRIPT_DIR}/.." > /dev/null

dotnet ef migrations "$@" \
  --context CodeArt.Poc.Storage.Postgresql.PostgresqlPocMainDbContext \
  --project ./src/CodeArt.Poc.Storage.Postgresql/CodeArt.Poc.Storage.Postgresql.csproj \
  --startup-project  ./src/CodeArt.Poc.Storage.Postgresql/CodeArt.Poc.Storage.Postgresql.csproj \
  --output-dir MainMigrations
  
  
popd > /dev/null