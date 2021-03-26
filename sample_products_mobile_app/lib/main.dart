import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/Config/app_config.dart';
import 'package:sample_products_mobile_app/app_starter.dart';
import 'package:sample_products_mobile_app/core/bloc/bloc_provider.dart';

void main() {
  runApp(
    BlocProvider(
      bloc: AppConfig(false,
          hasDatabase: true,
          baseApiUrl: "https://api-flutter-app.azurewebsites.net"),
      child: AppStarter(),
    ),
  );
}
