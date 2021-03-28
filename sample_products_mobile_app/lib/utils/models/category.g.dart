// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'category.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Category _$CategoryFromJson(Map<String, dynamic> json) {
  return Category(
    id: json['id'] as int?,
    name: json['name'] as String?,
    parentCategory: json['parentCategory'] == null
        ? null
        : Category.fromJson(json['parentCategory'] as Map<String, dynamic>),
    parentId: json['parentId'] as int?,
    pictureId: json['pictureId'] as int?,
    subCategories: (json['subCategories'] as List<dynamic>?)
        ?.map((e) => Category.fromJson(e as Map<String, dynamic>))
        .toList(),
  );
}

Map<String, dynamic> _$CategoryToJson(Category instance) => <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'parentId': instance.parentId,
      'pictureId': instance.pictureId,
      'parentCategory': instance.parentCategory,
      'subCategories': instance.subCategories,
    };
