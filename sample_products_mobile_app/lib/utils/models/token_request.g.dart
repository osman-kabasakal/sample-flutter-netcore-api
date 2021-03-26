// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'token_request.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

TokenRequest _$TokenRequestFromJson(Map<String, dynamic> json) {
  return TokenRequest(
    Email: json['Email'] as String,
    Password: json['Password'] as String,
  );
}

Map<String, dynamic> _$TokenRequestToJson(TokenRequest instance) =>
    <String, dynamic>{
      'Email': instance.Email,
      'Password': instance.Password,
    };
