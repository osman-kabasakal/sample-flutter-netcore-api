import 'package:flutter/cupertino.dart';
import 'package:sample_products_mobile_app/Config/routes/route_setting.dart';
import 'package:sample_products_mobile_app/constants/route_names.dart';

extension GetRoute on RouteSettings {
  Route getRoute() {
    if (routeNames.containsKey(this.name)) {
      return routeNames[this.name!]!(this);
    }
    return routeNames[Routes.home]!(this);
  }
}
