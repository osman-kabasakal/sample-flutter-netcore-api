import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/Config/app_config.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';
import 'package:sample_products_mobile_app/utils/models/api_response.dart';
import 'package:sample_products_mobile_app/utils/services/user_service.dart';

class CategoryManager {
  late AppConfig? appConfig;

  late UserService userService;

  CategoryManager(BuildContext context) {
    this.appConfig = context.getRequireBlocService<AppConfig>();
    this.userService = context.getRequireProviderService<UserService>();
  }

  Future<ApiResponse<List<Category>>> getCategoryTree()async{
    
  }
}
