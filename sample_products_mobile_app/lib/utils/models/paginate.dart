import 'package:json_annotation/json_annotation.dart';

part 'paginate.g.dart';

@JsonSerializable(genericArgumentFactories: true)
class Paginate<T> {
  late int from;

  late int index;

  late int size;

  late int count;

  late int pages;

  @JsonKey(name: "items")
  late List<T> items;

  late bool hasPrevious;

  late bool hasNext;

  Paginate({
    required this.from,
    required this.count,
    required this.hasNext,
    required this.hasPrevious,
    required this.index,
    required this.items,
    required this.pages,
    required this.size,
  });

  factory Paginate.fromJson(
          Map<String, dynamic> json, T Function(Object?) genericFactory) =>
      _$PaginateFromJson(json, genericFactory);
  Map<String, dynamic> toJson(T Function(Object?) genericFactory) =>
      _$PaginateToJson(this, genericFactory);
}
