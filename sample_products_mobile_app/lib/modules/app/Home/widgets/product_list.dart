import 'package:flutter/material.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
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
  void didUpdateWidget(covariant ProductList oldWidget) {
    super.didUpdateWidget(oldWidget);
  }

  @override
  void initState() {
    super.initState();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    this.productManager = context.getRequireProviderService<ProductManager>();
    this.appConfig = context.getRequireBlocService<AppConfig>()!;
  }

  int page = 0;
  bool pageLoading = false;
  ScrollController gridScroll = ScrollController(keepScrollOffset: true);
  double scrollPosition = 0.0;
  @override
  Widget build(BuildContext context) {
    return Container(
      width: 100,
      height: 100,
      padding: EdgeInsets.all(10),
      child: NotificationListener<ScrollNotification>(
        onNotification: (notification) {
          scrollPosition = notification.metrics.pixels;
          if (notification is ScrollEndNotification &&
              notification.metrics.maxScrollExtent ==
                  notification.metrics.pixels) {
            // if(pageLoading)

            // if (oynadiklari.length <= pageSize ||
            //     page * pageSize >= oynadiklari.length - 1) {
            //   setState(() {
            //     pageLoading = false;
            //   });
            //   return false;
            // }
            if (!pageLoading &&
                lasFetchedProducts != null &&
                lasFetchedProducts!.hasNext) {
              page++;
              setState(() {
                pageLoading = true;
              });
              return false;
            }
          }
          return true;
        },
        child: FutureBuilder<Paginate<Product>?>(
          future: productManager.getProducts(
              categoryId: widget.categoryId, page: page, size: 10),
          builder: (context, snapshot) {
            // if (snapshot.connectionState == ConnectionState.active ||
            //     snapshot.connectionState == ConnectionState.waiting)

            if (snapshot.connectionState == ConnectionState.done &&
                snapshot.hasData) {
              _setProducts(snapshot.data!);
              if (productList.length > 0) {
                return GridView.count(
                  controller: ScrollController(
                      keepScrollOffset: true,
                      initialScrollOffset: scrollPosition),
                  mainAxisSpacing: 10,
                  crossAxisCount: 2,
                  crossAxisSpacing: 10,
                  childAspectRatio: .8,
                  scrollDirection: Axis.vertical,
                  children: productList,
                  addAutomaticKeepAlives: true,
                );
              } else {
                return Center(
                  child: Container(
                    child: Center(
                      child: Text(
                        "Products is empty.",
                        style: Theme.of(context).textTheme.bodyText1,
                      ),
                    ),
                  ),
                );
              }
            } else if (snapshot.connectionState == ConnectionState.none &&
                !snapshot.hasData) {
              return Center(
                child: Container(
                  child: Center(
                    child: Text(
                      "Products is empty.",
                      style: Theme.of(context).textTheme.bodyText1,
                    ),
                  ),
                ),
              );
            }

            return Center(
              child: CircularProgressIndicator(),
            );
          },
        ),
      ),
    );
  }

  List<Widget> productList = [];
  Widget _productListItem(Product product) {
    var hasPicture =
        product.pictureIds != null && product.pictureIds!.length > 0;
    // var picture = ;
    return RawMaterialButton(
      onPressed: () {
        Navigator.of(context)
            .pushNamed(Routes.product, arguments: {"product": product});
      },
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
                decoration: hasPicture
                    ? BoxDecoration(
                        image: DecorationImage(
                            image: NetworkImage(appConfig.baseApiUrl +
                                "/mobile/images/" +
                                product.pictureIds!.first
                                    .toString()
                                    .padLeft(6, "0") +
                                ".webp"),
                            fit: BoxFit.fill))
                    : null,
                child: !hasPicture
                    ? SvgPicture.asset("assets/images/logo.svg")
                    : null,
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

  late Widget loadingWidget = Card(
    child: Visibility(
      child: ListTile(
        title: Center(
          child: CircularProgressIndicator(),
        ),
      ),
      visible: pageLoading,
    ),
  );
  Paginate<Product>? lasFetchedProducts;
  void _setProducts(Paginate<Product> paginate) {
    if (productList.contains(loadingWidget)) {
      productList.remove(loadingWidget);
    }
    lasFetchedProducts = paginate;
    if (paginate.items.length > 0) {
      productList.addAll(paginate.items.map((e) => _productListItem(e)));
      if (paginate.hasNext) {
        productList.add(loadingWidget);
      }
    }
    pageLoading = false;
  }
}
