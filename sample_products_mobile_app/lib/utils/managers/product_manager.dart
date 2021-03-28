import 'dart:convert';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/Config/app_config.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';
import 'package:sample_products_mobile_app/utils/models/api_response.dart';
import 'package:sample_products_mobile_app/utils/models/paginate.dart';
import 'package:sample_products_mobile_app/utils/models/product.dart';
import 'package:sample_products_mobile_app/utils/services/user_service.dart';
import 'package:http/http.dart' as http;

class ProductManager {
  late AppConfig? appConfig;

  late UserService userService;

  ProductManager(BuildContext context) {
    this.appConfig = context.getRequireBlocService<AppConfig>();
    this.userService = context.getRequireProviderService<UserService>();
  }

  Future<Paginate<Product>?> getProducts(
      {int? categoryId, int page = 0, int size = 20}) async {
    var query = categoryId != null
        ? "${appConfig?.baseApiUrl}/product/get/$categoryId?pageIndex=$page&pageSize=$size"
        : "${appConfig?.baseApiUrl}/product/get?pageIndex=$page&pageSize=$size";
    var uri = Uri.parse(query);
    var token = await userService.getToken();
    var response = await http.get(uri, headers: {
      // HttpHeaders.contentTypeHeader: "application/json",
      HttpHeaders.authorizationHeader: "Bearer $token",
      HttpHeaders.acceptCharsetHeader: "UTF-8"
    });
    if (response.statusCode != HttpStatus.ok) return null;
    final productResponse = ApiResponse<Paginate<Product>>.fromJson(
        jsonDecode(response.body),
        (json) => Paginate<Product>.fromJson(
            json as Map<String, dynamic>,
            (productJson) =>
                Product.fromJson(productJson as Map<String, dynamic>)));
    if (productResponse.isSuccessful!) {
      return productResponse.entity;
    }
    return null;
  }
}
