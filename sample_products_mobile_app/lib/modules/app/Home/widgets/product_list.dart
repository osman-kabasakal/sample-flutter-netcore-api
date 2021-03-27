import 'package:flutter/material.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:sample_products_mobile_app/Config/app_config.dart';
import 'package:sample_products_mobile_app/Config/routes/route_setting.dart';
import 'package:sample_products_mobile_app/constants/route_names.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';
import 'package:sample_products_mobile_app/utils/managers/product_manager.dart';
import 'package:sample_products_mobile_app/utils/models/paginate.dart';
import 'package:sample_products_mobile_app/utils/models/product.dart';
import 'package:sample_products_mobile_app/utils/ui/box_decorations.dart';

class ProductList extends StatefulWidget {
  int? categoryId;
  ProductList({this.categoryId});
  @override
  _ProductListState createState() => _ProductListState();
}

class _ProductListState extends State<ProductList> {
  late ProductManager productManager;

  late AppConfig appConfig;

  @override
  void didChangeDependencies() {
    // TODO: implement didChangeDependencies
    super.didChangeDependencies();
    this.productManager = context.getRequireProviderService<ProductManager>();
    this.appConfig = context.getRequireBlocService<AppConfig>()!;
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      width: 100,
      height: 100,
      padding: EdgeInsets.all(10),
      child: FutureBuilder<Paginate<Product>?>(
        future: productManager.getProducts(
            categoryId: widget.categoryId, page: 0, size: 10),
        builder: (context, snapshot) {
          if (snapshot.hasData) {
            if ((snapshot.data!.count) > 0) {
              return GridView.count(
                // gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
                //     crossAxisCount: 2,
                //     mainAxisSpacing: 10,
                //     mainAxisExtent: 2,
                //     crossAxisSpacing: 10),
                mainAxisSpacing: 10,
                crossAxisCount: 2,
                crossAxisSpacing: 10,
                childAspectRatio: .8,
                scrollDirection: Axis.vertical,
                children: snapshot.data!.items
                    .map((e) => _productListItem(e))
                    .toList(),
              );
            }
          }
          return Center(
            child: CircularProgressIndicator(),
          );
        },
      ),
    );
  }

  Widget _productListItem(Product product) {
    var picture = (product.pictureIds?.first == null
        ? AssetImage("")
        : NetworkImage(appConfig.baseApiUrl +
            "/images/" +
            product.pictureIds!.first.toString().padLeft(6, "0") +
            ".jpeg")) as ImageProvider;
    return RawMaterialButton(
      onPressed: () {},
      child: Container(
        width: 1000,
        height: 1000,
        clipBehavior: Clip.antiAlias,
        decoration: BoxDecoration(
          borderRadius: BorderRadius.all(Radius.elliptical(10, 10)),
          shape: BoxShape.rectangle,
        ),
        child: Stack(
          children: [
            Positioned.fill(
              child: Container(
                decoration: BoxDecoration(
                    image: DecorationImage(image: picture, fit: BoxFit.fill)),
              ),
            ),
            Positioned(
              bottom: 0,
              left: 0,
              width: MediaQuery.of(context).size.width * .48,
              height: 75,
              child: Container(
                  // height: double.infinity,
                  // width: double.infinity,
                  padding: EdgeInsets.symmetric(horizontal: 10, vertical: 10),
                  color: Colors.black26,
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Flexible(
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            Flexible(
                              flex: 1,
                              child: Text(
                                product.name ?? "none",
                                style: Theme.of(context)
                                    .textTheme
                                    .bodyText1
                                    ?.copyWith(
                                        color: Colors.white, fontSize: 10),
                              ),
                            ),
                            Flexible(
                              child: Text(
                                product.brand?.name ?? "brand",
                                style: Theme.of(context)
                                    .textTheme
                                    .bodyText2
                                    ?.copyWith(
                                        color: Colors.white, fontSize: 8),
                              ),
                            )
                          ],
                        ),
                      ),
                      Flexible(
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            Flexible(
                              child: Text(
                                product.price?.toString() ?? "none",
                                style: Theme.of(context)
                                    .textTheme
                                    .bodyText1
                                    ?.copyWith(
                                        color: Colors.white, fontSize: 10),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ],
                  )),
            ),
            Positioned(
              left: 10,
              top: 10,
              child: Icon(
                Icons.thumb_up_alt_outlined,
                color: Colors.orange,
              ),
            )
          ],
        ),
      ),
    );
  }
}
