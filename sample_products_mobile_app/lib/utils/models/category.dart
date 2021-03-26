import 'package:json_annotation/json_annotation.dart';

part 'category.g.dart';

@JsonSerializable()
class Category {
  late int? id;
  late String? name;
  late int? parentId;
  late int? pictureId;
  late Category? parentCategory;
  late List<Category>? subCategories;
  Category(
      {this.id,
      this.name,
      this.parentCategory,
      this.parentId,
      this.pictureId,
      this.subCategories});

  factory Category.fromJson(Map<String, dynamic> json) =>
      _$CategoryFromJson(json);
  Map<String, dynamic> toJson() => _$CategoryToJson(this);
}
