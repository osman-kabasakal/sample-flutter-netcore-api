import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:intl/intl.dart';
import 'package:sample_products_mobile_app/Config/app_config.dart';
import 'package:sample_products_mobile_app/modules/layouts/main_screen.dart';
import 'package:sample_products_mobile_app/utils/models/product.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';

class ProductScreen extends StatefulWidget {
  @override
  _ProductScreenState createState() => _ProductScreenState();
}

class _ProductScreenState extends State<ProductScreen> {
  late Product currentProduct;

  late AppConfig appConfig;
  @override
  Widget build(BuildContext context) {
    var routeSetting =
        ModalRoute.of(context)?.settings.arguments as Map<String, dynamic>;
    currentProduct = routeSetting["product"];
    appConfig = context.getRequireBlocService<AppConfig>()!;
    return MainScreen(
      _body(),
      _footer(),
      true,
      hasAppBar: false,
      footerSize: 1,
    );
  }

  final tryCurrencyFormat = NumberFormat.currency(
      customPattern: "#,##0.##", locale: "tr_TR", symbol: "₺");
  Widget _body() => Scaffold(
        body: Container(
            width: MediaQuery.of(context).size.width,
            child: ListView(
              children: [
                Container(
                  width: MediaQuery.of(context).size.width,
                  height: MediaQuery.of(context).size.height * .7,
                  child: Stack(
                    children: [
                      SizedBox.expand(
                        child: Container(
                          width: MediaQuery.of(context).size.width,
                          height: double.infinity,
                          child: PageView.builder(
                              itemCount: currentProduct.pictureIds?.length ?? 0,
                              itemBuilder: (ctx, index) {
                                return Container(
                                  width: double.infinity,
                                  height: double.infinity,
                                  clipBehavior: Clip.antiAlias,
                                  decoration: BoxDecoration(),
                                  child: Image(
                                    image: NetworkImage((appConfig.baseApiUrl +
                                        "/mobile/images/" +
                                        (currentProduct.pictureIds
                                                    ?.elementAt(index) ??
                                                0)
                                            .toString()
                                            .padLeft(6, "0") +
                                        ".webp")),
                                    fit: BoxFit.fitHeight,
                                    errorBuilder: (context, error, stackTrace) {
                                      return SvgPicture.asset(
                                          "assets/images/logo.svg");
                                    },
                                  ),
                                );
                              }),
                        ),
                      ),
                      Positioned(
                        left: 0,
                        top: 30,
                        child: Container(
                          width: MediaQuery.of(context).size.width,
                          padding: EdgeInsets.symmetric(vertical: 10),
                          child: Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              SizedBox(
                                child: RawMaterialButton(
                                  onPressed: () {
                                    Navigator.of(context).pop();
                                  },
                                  child: Container(
                                    width: 50,
                                    height: 50,
                                    padding: EdgeInsets.all(5),
                                    clipBehavior: Clip.antiAlias,
                                    decoration: BoxDecoration(
                                        shape: BoxShape.rectangle,
                                        color: Colors.white,
                                        borderRadius: BorderRadius.all(
                                            Radius.circular(5))),
                                    child: Icon(
                                      Icons.chevron_left_rounded,
                                      color: Colors.black,
                                    ),
                                  ),
                                ),
                              ),
                              SizedBox(
                                child: RawMaterialButton(
                                  onPressed: () {},
                                  child: Container(
                                    width: 50,
                                    height: 50,
                                    padding: EdgeInsets.all(5),
                                    clipBehavior: Clip.antiAlias,
                                    decoration: BoxDecoration(
                                        shape: BoxShape.rectangle,
                                        color: Colors.white,
                                        borderRadius: BorderRadius.all(
                                            Radius.circular(5))),
                                    child: Icon(
                                      Icons.shopping_cart,
                                      color: Colors.black,
                                    ),
                                  ),
                                ),
                              )
                            ],
                          ),
                        ),
                      )
                    ],
                  ),
                ),
                Container(
                  padding: EdgeInsets.all(10),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Container(
                        margin: EdgeInsets.symmetric(vertical: 10),
                        child: Text(
                          (currentProduct.name ?? "Ürün Adı"),
                          style: Theme.of(context)
                              .textTheme
                              .headline3
                              ?.copyWith(fontSize: 35, color: Colors.black),
                          textAlign: TextAlign.left,
                        ),
                      ),
                      Container(
                        // margin: EdgeInsets.symmetric(vertical: 5),
                        child: Text(
                          (currentProduct.brand?.name ?? "Ürün Adı"),
                          style: Theme.of(context)
                              .textTheme
                              .headline4
                              ?.copyWith(fontSize: 25),
                          textAlign: TextAlign.left,
                        ),
                      ),
                      Container(
                        margin: EdgeInsets.symmetric(vertical: 5),
                        width: double.infinity,
                        child: Text(
                          (tryCurrencyFormat.format(currentProduct.price ?? 0) +
                              tryCurrencyFormat.currencySymbol),
                          style: Theme.of(context)
                              .textTheme
                              .bodyText1
                              ?.copyWith(
                                  fontSize: 25, color: Colors.lightGreen),
                          textAlign: TextAlign.right,
                        ),
                      )
                    ],
                  ),
                ),
              ],
            )),
      );

  Widget _footer() => Container(
        color: Colors.white,
        width: MediaQuery.of(context).size.width,
        height: double.infinity,
        child: Row(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            Flexible(
              child: RawMaterialButton(
                  onPressed: () {},
                  child: Container(
                    margin: EdgeInsets.symmetric(vertical: 0),
                    padding: EdgeInsets.symmetric(vertical: 10, horizontal: 25),
                    decoration: BoxDecoration(
                      color: Colors.yellow[700],
                      shape: BoxShape.rectangle,
                      borderRadius: BorderRadius.all(Radius.circular(15)),
                    ),
                    child: Icon(Icons.add_shopping_cart_outlined),
                  )),
            ),
            Flexible(
              flex: 2,
              child: Container(
                margin: EdgeInsets.symmetric(vertical: 5),
                decoration: BoxDecoration(
                    color: Colors.orange,
                    shape: BoxShape.rectangle,
                    borderRadius: BorderRadius.all(Radius.circular(15))),
                child: RawMaterialButton(
                  onPressed: () {},
                  child: Center(
                    child: Text(
                      "Satın Al",
                      style: Theme.of(context).textTheme.button,
                    ),
                  ),
                ),
              ),
            ),
          ],
        ),
      );
}
