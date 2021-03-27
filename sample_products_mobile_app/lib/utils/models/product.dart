import 'package:json_annotation/json_annotation.dart';
import 'package:sample_products_mobile_app/utils/models/brand.dart';
import 'package:sample_products_mobile_app/utils/models/tag.dart';

part 'product.g.dart';

@JsonSerializable()
class Product {
  late int? id;
  late String? name;
  late String? shortDescription;
  late String? description;
  late int? quantity;
  late double? price;
  late int? brandId;

  late Brand? brand;
  late List<Tag>? tags;
  // late  ICollection<PictureModel> Pictures ;
  late List<int>? categoryIds;
  late List<int>? pictureIds;

  Product(
      {this.id,
      this.name,
      this.brand,
      this.brandId,
      this.categoryIds,
      this.description,
      this.pictureIds,
      this.price,
      this.quantity,
      this.shortDescription,
      this.tags});

  factory Product.fromJson(Map<String, dynamic> json) =>
      _$ProductFromJson(json);
  Map<String, dynamic> toJson() => _$ProductToJson(this);
}
