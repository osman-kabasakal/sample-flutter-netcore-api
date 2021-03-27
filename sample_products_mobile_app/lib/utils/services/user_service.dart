import 'dart:convert';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:rxdart/subjects.dart';
import 'package:sample_products_mobile_app/Config/app_config.dart';
import 'package:sample_products_mobile_app/constants/db_insert_confilict_action.dart';
import 'package:sample_products_mobile_app/core/bloc/reactive_bloc.dart';
import 'package:sample_products_mobile_app/core/domain/entities/user.dart';
import 'package:sample_products_mobile_app/core/domain/repositories/user_repository.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';
import 'package:http/http.dart' as http;
import 'package:sample_products_mobile_app/utils/models/api_response.dart';
import 'package:sample_products_mobile_app/utils/models/token_request.dart';

class UserService {
  late UserRepository userRepo;

  ReactiveSubject<User>? user;

  BehaviorSubject<User>? userSubject;

  late AppConfig appConfig;

  UserService(BuildContext context) {
    this.userRepo = context.getRequireProviderService<UserRepository>();
    this.user = context.getRequireReactiveValue<User>();
    this.userSubject = (this.user?.subject as BehaviorSubject<User>);
    this.appConfig = context.getRequireBlocService<AppConfig>() ??
        new AppConfig(true,
            hasDatabase: false, baseApiUrl: "https://10.0.2.2:8080");
  }

  Future<String>? getToken() async {
    if (this.userSubject != null) {
      var lastUser = this.userSubject!.valueWrapper?.value;
      return lastUser?.jwtToken ?? "";
    }
    return "";
  }

  Future<User?> signIn(String email, String password) async {
    var uri = Uri.parse("${appConfig.baseApiUrl}/token/generate");
    try {
      var headers = <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      };
      final response = await http.post(uri,
          body: jsonEncode(
              TokenRequest(Email: email, Password: password).toJson()),
          headers: headers);
      if (response.statusCode == HttpStatus.ok) {
        var userResponse = ApiResponse<User>.fromJson(jsonDecode(response.body),
            (json) => User.fromJson(json as Map<String, dynamic>));
        if (userResponse.isSuccessful != null && userResponse.isSuccessful!) {
          if (userResponse.entity != null) {
            if (appConfig.hasDatabase)
              await userRepo.addAll(
                  userResponse.entity!.toSqlite().keys.toList(),
                  [userResponse.entity!],
                  DbInsertConfilictExtion.replace);
            userSubject?.add(userResponse.entity!);
          }
          return userResponse.entity;
        }
      }
    } catch (e) {}

    return null;
  }
}
