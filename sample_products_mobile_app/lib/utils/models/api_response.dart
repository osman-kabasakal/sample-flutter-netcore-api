import 'package:json_annotation/json_annotation.dart';

part 'api_response.g.dart';

@JsonSerializable(genericArgumentFactories: true)
class ApiResponse<T> {
  bool? hasExceptionError;
  String? exceptionMessage;
  bool? isSuccessful;
  T? entity;
  int? count;
  bool? isValid;

  ApiResponse(
      {this.hasExceptionError,
      this.count,
      this.entity,
      this.exceptionMessage,
      this.isSuccessful,
      this.isValid});

  factory ApiResponse.fromJson(
          Map<String, dynamic> json, T Function(Object?) convert) =>
      _$ApiResponseFromJson(json, convert);
  Map<String, dynamic> toJson(T Function(Object?) convert) =>
      _$ApiResponseToJson(this, convert);
}
