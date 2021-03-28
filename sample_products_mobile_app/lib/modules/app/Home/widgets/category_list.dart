import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/Config/app_config.dart';
import 'package:sample_products_mobile_app/Config/routes/route_setting.dart';
import 'package:sample_products_mobile_app/constants/route_names.dart';
import 'package:sample_products_mobile_app/utils/managers/categoryManager.dart';
import 'package:sample_products_mobile_app/utils/models/category.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';

class CategoryList extends StatefulWidget {
  int? categoryId;
  CategoryList({this.categoryId});

  @override
  _CategoryListState createState() => _CategoryListState();
}

class _CategoryListState extends State<CategoryList> {
  late AppConfig appConfig;

  late CategoryManager categoryManager;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    this.appConfig = context.getRequireBlocService<AppConfig>() as AppConfig;
    this.categoryManager = context.getRequireProviderService<CategoryManager>();
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<Category>?>(
      future: categoryManager.getCategoryTree(),
      builder: (cotegoryCtx, snapshout) {
        if (snapshout.hasData) {
          var cat = widget.categoryId != null
              ? snapshout.data
                  ?.where((element) => element.id == widget.categoryId)
              : snapshout.data?.where((element) => element.parentId == null);
          var hasItem = (cat?.toList().length ?? 0) > 0;
          if (widget.categoryId != null && hasItem) {
            cat = cat!.first.subCategories;
          }
          print(hasItem);
          if (!hasItem) {
            return SizedBox.shrink();
          }
          return ListView(
              scrollDirection: Axis.horizontal,
              children: cat!.toList().map((e) => _categoryItem(e)).toList());
        }
        return Container(
          width: 10,
          height: 10,
          child: Center(
            child: CircularProgressIndicator(),
          ),
        );
      },
    );
  }

  Widget _categoryItem(Category category) {
    return Container(
      // width: 100,
      height: 10,
      padding: EdgeInsets.all(12.5),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Container(
            width: 50,
            height: 50,
            clipBehavior: Clip.antiAlias,
            decoration: BoxDecoration(
              shape: BoxShape.circle,
            ),
            child: RawMaterialButton(
              onPressed: () {
                Navigator.of(context).push(routeNames[Routes.home]!(
                    RouteSettings(
                        name: Routes.home,
                        arguments: {"categoryId": category.id})));
              },
              child: Image.network(
                "${appConfig.baseApiUrl}/mobile/images/${category.pictureId.toString().padLeft(6, '0')}.webp",
                fit: BoxFit.fitWidth,
              ),
            ),
          ),
          Container(
            margin: EdgeInsets.only(top: 5),
            child: Text(
              category.name ?? "1",
              style: Theme.of(context)
                  .textTheme
                  .caption
                  ?.copyWith(color: Colors.black, fontWeight: FontWeight.bold),
            ),
          )
        ],
      ),
    );
  }
}
