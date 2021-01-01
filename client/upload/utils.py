from pathlib import Path
from typing import Dict, Iterator, Any, Optional, Tuple

from ..grpc_types import dataUpload_pb2


_dtype_mapping: Dict[str, Tuple[int, str]] = {
    "int": (dataUpload_pb2.Int64, "int64Data"),
    "float": (dataUpload_pb2.Double, "double32Data"),
    "str": (dataUpload_pb2.String, "stringData"),
}


def _build_kwargs(path: Path, key: str, value: Any) -> Optional[dataUpload_pb2.DataRequest]:
    value_array = value if isinstance(value, list) else [value]
    mapping = _dtype_mapping.get(type(value_array[0]))
    if mapping is None:
        return None
    dtype, data_key = mapping

    kwargs = {
        "path": str(path),
        key: key,
        "dtype": dtype,
        "dim": len(value_array),
        "stringData": [],
        "int32Data": [],
        "int64Data": [],
        "double32Data": [],
        data_key: value_array
    }
    return dataUpload_pb2.DataRequest(**kwargs)


def _flatten_dict(path: Path, parent_key: str, data_dict: Dict) -> Iterator[dataUpload_pb2.DataRequest]:
    for key, value in data_dict.items():
        child_key = f"{parent_key}.{key}"
        if isinstance(value, dict):
            yield from _flatten_dict(path, child_key, value)
        else:
            yield _build_kwargs(path, child_key, value)


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
    result = _flatten_dict(path, "", mock_data)
    return result
