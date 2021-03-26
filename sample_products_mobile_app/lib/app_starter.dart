import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:sample_products_mobile_app/core/di/di_level.dart';
import 'package:sample_products_mobile_app/utils/ui/box_decorations.dart';

import 'constants/route_names.dart';
import 'core/bloc/bloc_provider.dart';
import 'core/bloc/reactive_variebles.dart';
import 'core/domain/context/context.dart';
import 'core/domain/repositories/user_repository.dart';
import 'modules/starter/repository_level.dart';
import 'utils/helpers/route_helpers.dart';

class AppStarter extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      bloc: ReactiveVariebles(),
      child: BlocProvider(
        bloc: DatabaseContext(),
        child: RepositoriesLevel(
          next: DiLevel(
            order: 1,
            child: (chCtx) => DiLevel(
              order: 2,
              child: (context) => MaterialApp(
                title: "Selam",
                initialRoute: Routes.home,
                onGenerateRoute: (settings) => settings.getRoute(),
              ),
              depends: [
                (ctx) => UserRepository(ctx),
              ],
            ),
            depends: [
              (ctx) => BoxDecorations(),
            ],
          ),
        ),
      ),
    );
  }
}
