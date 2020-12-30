# CosmoResearch
CosmoDB research repository

### Generating client proto files

```console
python -m grpc_tools.protoc -I Protos --python_out=client --grpc_python_out=client Protos/greet.proto
```
