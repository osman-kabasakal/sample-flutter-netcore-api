import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/Config/routes/page_routes.dart';
import 'package:sample_products_mobile_app/constants/route_names.dart';
import 'package:sample_products_mobile_app/modules/app/Home/home.dart';

final Map<String, Route Function(RouteSettings)> routeNames = {
  Routes.home: (route) => PageRoutes.fade<Home>(() => Home(), route)
};
