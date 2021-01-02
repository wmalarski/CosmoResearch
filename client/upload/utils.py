from pathlib import Path
from typing import Dict, Iterator, Any, Optional, Tuple, List, Type

from ..grpc_types import dataUpload_pb2


_dtype_mapping: Dict[Type, Tuple[int, str]] = {
    int: (dataUpload_pb2.Int64, "int64Data"),
    float: (dataUpload_pb2.Double, "double32Data"),
    str: (dataUpload_pb2.String, "stringData"),
}


def _build_request(path_key: str, key: List[str], value: Any) -> Optional[dataUpload_pb2.DataRequest]:
    value_array = value if isinstance(value, list) else [value]
    mapping = _dtype_mapping.get(type(value_array[0]))
    if mapping is None:
        return None
    dtype, data_key = mapping
    return dataUpload_pb2.DataRequest(**{
        "path": path_key,
        "key": ".".join(key),
        "dataType": dtype,
        "dim": [len(value_array)],
        "stringData": [],
        "int32Data": [],
        "int64Data": [],
        "double32Data": [],
        data_key: value_array
    })


def _flatten_dict(path_key: str, parent_key: List[str], data_dict: Dict) -> Iterator[dataUpload_pb2.DataRequest]:
    for key, value in data_dict.items():
        child_key = [*parent_key, key]
        if isinstance(value, dict):
            yield from _flatten_dict(path_key, child_key, value)
        else:
            request = _build_request(path_key, child_key, value)
            if request is not None:
                yield request


def load_data(path: Path) -> Iterator[dataUpload_pb2.DataRequest]:
    mock_data = {
        "path": str(path),
        "000": "dsAdas",
        "aaa": ["ds", "dsa"],
        "bbb": {
            "bb2": [4, 5, 6, 2],
            "bb3": [3.5, 2.1, 4.5],
            "dsd": {
                "aa": ["ds", "bcf"],
                "bc": [3, 6, 7, 3, 2]
            }
        }
    }
    path_name = path.with_suffix("").name
    return _flatten_dict(path_name, [], mock_data)
