import 'dart:convert';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/Config/app_config.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';
import 'package:sample_products_mobile_app/utils/models/api_response.dart';
import 'package:sample_products_mobile_app/utils/models/category.dart';
import 'package:sample_products_mobile_app/utils/services/user_service.dart';
import 'package:http/http.dart' as http;

class CategoryManager {
  late AppConfig? appConfig;

  late UserService userService;

  CategoryManager(BuildContext context) {
    this.appConfig = context.getRequireBlocService<AppConfig>();
    this.userService = context.getRequireProviderService<UserService>();
  }

  Future<List<Category>?> getCategoryTree() async {
    var uri = Uri.parse("${appConfig?.baseApiUrl}/category/getCategoryTree");
    var token = await userService.getToken();
    var response = await http.get(uri, headers: {
      // HttpHeaders.contentTypeHeader: "application/json",
      HttpHeaders.authorizationHeader: "Bearer $token",
      HttpHeaders.acceptCharsetHeader: "UTF-8"
    });
    if (response.statusCode != HttpStatus.ok) return [];

    var categoryResponse = ApiResponse<List<Category>>.fromJson(
        jsonDecode(response.body),
        (json) => (json as List<dynamic>)
            .map((e) => Category.fromJson(e as Map<String, dynamic>))
            .toList());
    if (categoryResponse.isSuccessful!) {
      return categoryResponse.entity;
    }
    return [];
  }
}
