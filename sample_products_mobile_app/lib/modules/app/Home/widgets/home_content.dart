import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/modules/app/Home/widgets/category_list.dart';
import 'package:sample_products_mobile_app/modules/app/Home/widgets/product_list.dart';

class HomeContent extends StatefulWidget {
  @override
  _HomeContentState createState() => _HomeContentState();
}

class _HomeContentState extends State<HomeContent> {
  @override
  Widget build(BuildContext context) {
    final routeArgs =
        ModalRoute.of(context)?.settings.arguments as Map<String, dynamic>?;
    return Container(
      width: MediaQuery.of(context).size.width,
      height: MediaQuery.of(context).size.height,
      child: ListView(
        clipBehavior: Clip.antiAlias,
        children: [
          Container(
            width: double.infinity,
            height: 150,
            child: CategoryList(
              categoryId: routeArgs?["categoryId"],
            ),
          ),
          Container(
            width: MediaQuery.of(context).size.width,
            height: MediaQuery.of(context).size.height * .6,
            child: ProductList(
              categoryId: routeArgs?["categoryId"],
            ),
          )
        ],
      ),
    );
  }
}
