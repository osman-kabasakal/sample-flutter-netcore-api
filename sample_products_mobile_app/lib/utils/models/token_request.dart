import 'package:json_annotation/json_annotation.dart';

part 'token_request.g.dart';

@JsonSerializable()
class TokenRequest {
  final String Email;
  final String Password;
  TokenRequest({required this.Email, required this.Password});

  factory TokenRequest.fromJson(Map<String, dynamic> json) =>
      _$TokenRequestFromJson(json);
  Map<String, dynamic> toJson() => _$TokenRequestToJson(this);
}
