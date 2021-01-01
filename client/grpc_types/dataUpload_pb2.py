# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: dataUpload.proto
"""Generated protocol buffer code."""
from google.protobuf.internal import enum_type_wrapper
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='dataUpload.proto',
  package='dataUpload',
  syntax='proto3',
  serialized_options=b'\252\002\rCosmoResearch',
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x10\x64\x61taUpload.proto\x12\ndataUpload\"\xab\x01\n\x0b\x44\x61taRequest\x12\x0c\n\x04path\x18\x01 \x01(\t\x12\x0b\n\x03key\x18\x02 \x01(\t\x12$\n\x05\x64type\x18\x03 \x01(\x0e\x32\x15.dataUpload.DataDType\x12\x0b\n\x03\x64im\x18\x04 \x01(\r\x12\x12\n\nstringData\x18\x05 \x03(\t\x12\x11\n\tint32Data\x18\x06 \x03(\x05\x12\x11\n\tint64Data\x18\x07 \x03(\x03\x12\x14\n\x0c\x64ouble32Data\x18\x08 \x03(\x01\"\x1c\n\tDataReply\x12\x0f\n\x07success\x18\x01 \x01(\t*9\n\tDataDType\x12\n\n\x06String\x10\x00\x12\t\n\x05Int32\x10\x01\x12\t\n\x05Int64\x10\x02\x12\n\n\x06\x44ouble\x10\x03\x32J\n\nDataUpload\x12<\n\x08SendData\x12\x17.dataUpload.DataRequest\x1a\x15.dataUpload.DataReply(\x01\x42\x10\xaa\x02\rCosmoResearchb\x06proto3'
)

_DATADTYPE = _descriptor.EnumDescriptor(
  name='DataDType',
  full_name='dataUpload.DataDType',
  filename=None,
  file=DESCRIPTOR,
  create_key=_descriptor._internal_create_key,
  values=[
    _descriptor.EnumValueDescriptor(
      name='String', index=0, number=0,
      serialized_options=None,
      type=None,
      create_key=_descriptor._internal_create_key),
    _descriptor.EnumValueDescriptor(
      name='Int32', index=1, number=1,
      serialized_options=None,
      type=None,
      create_key=_descriptor._internal_create_key),
    _descriptor.EnumValueDescriptor(
      name='Int64', index=2, number=2,
      serialized_options=None,
      type=None,
      create_key=_descriptor._internal_create_key),
    _descriptor.EnumValueDescriptor(
      name='Double', index=3, number=3,
      serialized_options=None,
      type=None,
      create_key=_descriptor._internal_create_key),
  ],
  containing_type=None,
  serialized_options=None,
  serialized_start=236,
  serialized_end=293,
)
_sym_db.RegisterEnumDescriptor(_DATADTYPE)

DataDType = enum_type_wrapper.EnumTypeWrapper(_DATADTYPE)
String = 0
Int32 = 1
Int64 = 2
Double = 3



_DATAREQUEST = _descriptor.Descriptor(
  name='DataRequest',
  full_name='dataUpload.DataRequest',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='path', full_name='dataUpload.DataRequest.path', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='key', full_name='dataUpload.DataRequest.key', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='dtype', full_name='dataUpload.DataRequest.dtype', index=2,
      number=3, type=14, cpp_type=8, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='dim', full_name='dataUpload.DataRequest.dim', index=3,
      number=4, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='stringData', full_name='dataUpload.DataRequest.stringData', index=4,
      number=5, type=9, cpp_type=9, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='int32Data', full_name='dataUpload.DataRequest.int32Data', index=5,
      number=6, type=5, cpp_type=1, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='int64Data', full_name='dataUpload.DataRequest.int64Data', index=6,
      number=7, type=3, cpp_type=2, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='double32Data', full_name='dataUpload.DataRequest.double32Data', index=7,
      number=8, type=1, cpp_type=5, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=33,
  serialized_end=204,
)


_DATAREPLY = _descriptor.Descriptor(
  name='DataReply',
  full_name='dataUpload.DataReply',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='success', full_name='dataUpload.DataReply.success', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=206,
  serialized_end=234,
)

_DATAREQUEST.fields_by_name['dtype'].enum_type = _DATADTYPE
DESCRIPTOR.message_types_by_name['DataRequest'] = _DATAREQUEST
DESCRIPTOR.message_types_by_name['DataReply'] = _DATAREPLY
DESCRIPTOR.enum_types_by_name['DataDType'] = _DATADTYPE
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

DataRequest = _reflection.GeneratedProtocolMessageType('DataRequest', (_message.Message,), {
  'DESCRIPTOR' : _DATAREQUEST,
  '__module__' : 'dataUpload_pb2'
  # @@protoc_insertion_point(class_scope:dataUpload.DataRequest)
  })
_sym_db.RegisterMessage(DataRequest)

DataReply = _reflection.GeneratedProtocolMessageType('DataReply', (_message.Message,), {
  'DESCRIPTOR' : _DATAREPLY,
  '__module__' : 'dataUpload_pb2'
  # @@protoc_insertion_point(class_scope:dataUpload.DataReply)
  })
_sym_db.RegisterMessage(DataReply)


DESCRIPTOR._options = None

_DATAUPLOAD = _descriptor.ServiceDescriptor(
  name='DataUpload',
  full_name='dataUpload.DataUpload',
  file=DESCRIPTOR,
  index=0,
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_start=295,
  serialized_end=369,
  methods=[
  _descriptor.MethodDescriptor(
    name='SendData',
    full_name='dataUpload.DataUpload.SendData',
    index=0,
    containing_service=None,
    input_type=_DATAREQUEST,
    output_type=_DATAREPLY,
    serialized_options=None,
    create_key=_descriptor._internal_create_key,
  ),
])
_sym_db.RegisterServiceDescriptor(_DATAUPLOAD)

DESCRIPTOR.services_by_name['DataUpload'] = _DATAUPLOAD

# @@protoc_insertion_point(module_scope)