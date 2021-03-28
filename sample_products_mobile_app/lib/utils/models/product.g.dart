// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'product.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Product _$ProductFromJson(Map<String, dynamic> json) {
  return Product(
    id: json['id'] as int?,
    name: json['name'] as String?,
    brand: json['brand'] == null
        ? null
        : Brand.fromJson(json['brand'] as Map<String, dynamic>),
    brandId: json['brandId'] as int?,
    categoryIds:
        (json['categoryIds'] as List<dynamic>?)?.map((e) => e as int).toList(),
    description: json['description'] as String?,
    pictureIds:
        (json['pictureIds'] as List<dynamic>?)?.map((e) => e as int).toList(),
    price: (json['price'] as num?)?.toDouble(),
    quantity: json['quantity'] as int?,
    shortDescription: json['shortDescription'] as String?,
    tags: (json['tags'] as List<dynamic>?)
        ?.map((e) => Tag.fromJson(e as Map<String, dynamic>))
        .toList(),
  );
}

Map<String, dynamic> _$ProductToJson(Product instance) => <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'shortDescription': instance.shortDescription,
      'description': instance.description,
      'quantity': instance.quantity,
      'price': instance.price,
      'brandId': instance.brandId,
      'brand': instance.brand,
      'tags': instance.tags,
      'categoryIds': instance.categoryIds,
      'pictureIds': instance.pictureIds,
    };
