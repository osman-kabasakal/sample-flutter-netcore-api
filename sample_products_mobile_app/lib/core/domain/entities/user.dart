import 'package:sample_products_mobile_app/core/domain/abstract/base_entity.dart';
import 'package:json_annotation/json_annotation.dart';

part 'user.g.dart';

@JsonSerializable()
class User extends IEntity {
  User({required this.id, this.email, this.jwtToken, this.expire});

  final String id;
  final String? email;
  final String? jwtToken;

  int? expire;

  DateTime get expireTime {
    return DateTime.fromMillisecondsSinceEpoch(
        this.expire ?? DateTime.now().millisecondsSinceEpoch);
  }

  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
  Map<String, dynamic> toJson() => _$UserToJson(this);

  @override
  Map<String, dynamic> toSqlite() => _$UserToJson(this);
}
