// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'paginate.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Paginate<T> _$PaginateFromJson<T>(
  Map<String, dynamic> json,
  T Function(Object? json) fromJsonT,
) {
  return Paginate<T>(
    from: json['from'] as int,
    count: json['count'] as int,
    hasNext: json['hasNext'] as bool,
    hasPrevious: json['hasPrevious'] as bool,
    index: json['index'] as int,
    items: (json['items'] as List<dynamic>).map(fromJsonT).toList(),
    pages: json['pages'] as int,
    size: json['size'] as int,
  );
}

Map<String, dynamic> _$PaginateToJson<T>(
  Paginate<T> instance,
  Object? Function(T value) toJsonT,
) =>
    <String, dynamic>{
      'from': instance.from,
      'index': instance.index,
      'size': instance.size,
      'count': instance.count,
      'pages': instance.pages,
      'items': instance.items.map(toJsonT).toList(),
      'hasPrevious': instance.hasPrevious,
      'hasNext': instance.hasNext,
    };
