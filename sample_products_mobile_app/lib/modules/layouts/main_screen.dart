import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/widgets/appbar_state_less.dart';

import 'main_layout.dart';

class MainScreen extends MainLayout {
  MainScreen(Widget body, Widget footer, bool footerActive,
      {int contentSize = 10,
      int footerSize = 2,
      String title = "",
      bool footerBgLight = true})
      : super(_appBar(title: title), _content(child: body),
            _footer(child: footer, bgLight: footerBgLight),
            contentSize: contentSize,
            footerSize: footerSize,
            footerActive: footerActive);

  static Widget _appBar({String title = ""}) {
    // return AppBar(title: title);
    return AppBarStLess(title);
  }

  static Widget _content({Widget child = const SizedBox.shrink()}) {
    return Container(
      margin: EdgeInsets.only(bottom: 10),
      // decoration: BoxDecoration(
      //   image: DecorationImage(
      //     image: AssetImage("assets/game_area.png"),
      //     fit: BoxFit.fill,
      //   ),
      // ),
      child: child,
    );
  }

  static Widget _footer(
      {Widget child = const SizedBox.shrink(), bool bgLight = false}) {
    return child;
  }
}
