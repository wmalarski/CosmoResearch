from pathlib import Path

import grpc

from .utils import load_data
from ..grpc_types import dataUpload_pb2_grpc


def run(channel: str, path: Path):
    with grpc.insecure_channel(channel) as channel:
        stub = dataUpload_pb2_grpc.DataUploadStub(channel)
        data_iterator = load_data(path)
        upload_result = stub.SendData(data_iterator)
        print(upload_result)
