// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'api_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ApiResponse<T> _$ApiResponseFromJson<T>(
  Map<String, dynamic> json,
  T Function(Object? json) fromJsonT,
) {
  return ApiResponse<T>(
    hasExceptionError: json['hasExceptionError'] as bool?,
    count: json['count'] as int?,
    entity: _$nullableGenericFromJson(json['entity'], fromJsonT),
    exceptionMessage: json['exceptionMessage'] as String?,
    isSuccessful: json['isSuccessful'] as bool?,
    isValid: json['isValid'] as bool?,
  );
}

Map<String, dynamic> _$ApiResponseToJson<T>(
  ApiResponse<T> instance,
  Object? Function(T value) toJsonT,
) =>
    <String, dynamic>{
      'hasExceptionError': instance.hasExceptionError,
      'exceptionMessage': instance.exceptionMessage,
      'isSuccessful': instance.isSuccessful,
      'entity': _$nullableGenericToJson(instance.entity, toJsonT),
      'count': instance.count,
      'isValid': instance.isValid,
    };

T? _$nullableGenericFromJson<T>(
  Object? input,
  T Function(Object? json) fromJson,
) =>
    input == null ? null : fromJson(input);

Object? _$nullableGenericToJson<T>(
  T? input,
  Object? Function(T value) toJson,
) =>
    input == null ? null : toJson(input);
