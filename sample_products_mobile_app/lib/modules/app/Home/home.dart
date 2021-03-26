import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/modules/layouts/main_screen.dart';

class Home extends StatefulWidget {
  @override
  _HomeState createState() => _HomeState();
}

class _HomeState extends State<Home> {
  @override
  Widget build(BuildContext context) {
    return MainScreen(_body(), SizedBox.shrink(), true);
  }

  Widget _body() => Container(
        decoration: BoxDecoration(color: Colors.white),
        child: Center(
          child: Text(
            "Deneme",
            style:
                Theme.of(context).textTheme.bodyText2?.copyWith(fontSize: 21),
          ),
        ),
      );
}
