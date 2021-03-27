import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:provider/provider.dart';
import 'package:sample_products_mobile_app/core/di/di_level.dart';
import 'package:sample_products_mobile_app/utils/managers/categoryManager.dart';
import 'package:sample_products_mobile_app/utils/managers/product_manager.dart';
import 'package:sample_products_mobile_app/utils/services/user_service.dart';
import 'package:sample_products_mobile_app/utils/ui/box_decorations.dart';

import 'constants/route_names.dart';
import 'core/bloc/bloc_provider.dart';
import 'core/bloc/reactive_variebles.dart';
import 'core/domain/repositories/user_repository.dart';
import 'modules/starter/repository_level.dart';
import 'utils/helpers/route_helpers.dart';

class AppStarter extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      bloc: ReactiveVariebles(),
      child: RepositoriesLevel(
        next: DiLevel(
          order: 1,
          child: (chCtx) => DiLevel(
              order: 2,
              child: (context) => DiLevel(
                    order: 3,
                    child: (dicontext3) => DiLevel(
                        order: 4,
                        child: (di3Context) => MaterialApp(
                              title: "Selam",
                              initialRoute: Routes.home,
                              onGenerateRoute: (settings) =>
                                  settings.getRoute(),
                            ),
                        depends: [
                          (ctx) => Provider<ProductManager>(
                              create: (_) => ProductManager(ctx)),
                          (ctx) => Provider<CategoryManager>(
                              create: (_) => CategoryManager(ctx)),
                        ]),
                    depends: [
                      (ctx) => Provider<UserService>(
                          create: (_) => UserService(ctx)),
                    ],
                  ),
              depends: [
                (ctx) => Provider<UserRepository>(
                    create: (_) => UserRepository(ctx)),
              ]),
          depends: [
            (ctx) => Provider<BoxDecorations>(create: (_) => BoxDecorations())
          ],
        ),
      ),
    );
  }
}
